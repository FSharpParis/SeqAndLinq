module Generate

open SeqAndLinq

let intro = @"
# Mappings between F# Seq and LINQ

This is an attempt do build an exhaustive list of mappings between
LINQ's extension methods on `IEnumerable<T>` and F# sequence manipulation
functions, in order to ease the transition for C# devs.

The current list is absolutely **NOT** complete. Running the
unit tests will tell you which mappings are missing, so please
contribute!
"

type TypeSigElement =
    | ValueType of string
    | FuncType of TypeSigElement list with
    override this.ToString() =
        match this with
        | ValueType name -> name
        | FuncType types ->
            let rec wrap types = seq {
                match types with
                | [] -> ()
                | [t] -> yield t.ToString()
                | t :: ts ->
                    match t with
                    | ValueType _ -> yield t.ToString()
                    | FuncType _ -> yield sprintf "(%O)" t
                    yield! wrap ts }
            wrap types |> String.concat " -> "

let getTypeSignature (func:System.Reflection.MethodInfo) =
    let rec getTypeSignature' (types: System.Type seq) = seq {
        for t in types do
            if t.IsGenericType && t.Name = "IEnumerable`1" then
                yield t.GetGenericArguments().[0].Name |> sprintf "seq<%s>" |> ValueType
            elif t.IsGenericType && t.Name.StartsWith("FSharpFunc`") then
                yield t.GetGenericArguments() |> getTypeSignature' |> Seq.toList |> FuncType
            elif t.Name = "Boolean" then
                yield ValueType "bool"
            else
                yield t.Name |> ValueType
    }

    seq {
        yield! func.GetParameters() |> Seq.map (fun p -> p.ParameterType)
        yield func.ReturnType }
    |> getTypeSignature'
    |> Seq.toList
    |> FuncType

let markdown =

    let seqFuncs =
        let tryGetFunctionName (mi:System.Reflection.MemberInfo) =
            mi.CustomAttributes
            |> Seq.tryFind (fun a -> a.AttributeType = typeof<FSharp.Core.CompilationSourceNameAttribute>)
            |> Option.map (fun a -> a.ConstructorArguments.[0].Value :?> string)

        typeof<Set<string>>
            .Assembly
            .GetType("Microsoft.FSharp.Collections.SeqModule")
            .GetMethods(System.Reflection.BindingFlags.Static ||| System.Reflection.BindingFlags.Public)
        |> Seq.choose (fun m -> tryGetFunctionName m |> Option.map (fun n -> n, m))
        |> Map.ofSeq

    seq {
        yield intro
        yield "|LINQ methods|F# Seq functions|Explanation|"
        yield "|---|---|---|"
        for mapping in mappings do
            let linqSide =
                sprintf "**%s**<br>Ex: `%s`" mapping.LinqMethod.Name mapping.LinqMethod.Sample

            let seqSide =
                if mapping.SeqFunc.Name <> "" then
                    let func = seqFuncs.[mapping.SeqFunc.Name]
                    let typeSignature = getTypeSignature func
                    sprintf "**%s**<br>`%O`<br>Ex: `%s`" mapping.SeqFunc.Name typeSignature mapping.SeqFunc.Sample
                else
                    sprintf "`%s`" mapping.SeqFunc.Sample

            yield sprintf "|%s|%s|%s|" linqSide seqSide mapping.Explanation
    }

let outputFile = System.IO.Path.Combine(__SOURCE_DIRECTORY__, "..", "README.md")
System.IO.File.WriteAllLines(outputFile, markdown)
