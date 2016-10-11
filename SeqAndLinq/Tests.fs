module Tests

open NUnit.Framework
open FsUnit

open SeqAndLinq

type [<TestFixture>] Tests () =
    member this.seqFuncs =
        let tryGetFunctionName (mi:System.Reflection.MethodInfo) =
            mi.CustomAttributes
            |> Seq.tryFind (fun a -> a.AttributeType = typeof<FSharp.Core.CompilationSourceNameAttribute>)
            |> Option.map (fun a -> a.ConstructorArguments.[0].Value :?> string)

        typeof<Set<string>>
            .Assembly
            .GetType("Microsoft.FSharp.Collections.SeqModule")
            .GetMethods(System.Reflection.BindingFlags.Static ||| System.Reflection.BindingFlags.Public)
        |> Seq.choose tryGetFunctionName
        |> Seq.toArray

    member this.linqMethods =
        typeof<System.Linq.Enumerable>
            .GetMethods(System.Reflection.BindingFlags.Static ||| System.Reflection.BindingFlags.Public)
        |> Seq.filter (fun m -> m.CustomAttributes |> Seq.exists (fun a -> a.AttributeType = typeof<System.Runtime.CompilerServices.ExtensionAttribute>))
        |> Seq.map (fun m -> m.Name)
        |> Seq.distinct
        |> Seq.toArray

    [<TestCaseSource("seqFuncs")>]
    member this.``Seq function has a mapping`` (funcName:string) =
        SeqAndLinq.mappings
        |> Seq.map (fun m -> m.SeqFunc.Name)
        |> shouldContain funcName

    [<TestCaseSource("linqMethods")>]
    member this.``Linq method has a mapping`` (methodName:string) =
        SeqAndLinq.mappings
        |> Seq.map (fun m -> m.LinqMethod.Name)
        |> shouldContain methodName
