module Tests

open NUnit.Framework
open FsUnit

open SeqAndLinq

type [<TestFixture>] Tests () =
    member this.seqFuncs =
        let tryGetFunctionName (mi:System.Reflection.MemberInfo) =
            mi.CustomAttributes
            |> Seq.tryFind (fun a -> a.AttributeType = typeof<FSharp.Core.CompilationSourceNameAttribute>)
            |> Option.map (fun a -> a.ConstructorArguments.[0].Value :?> string)

        typeof<Set<string>>
            .Assembly
            .GetType("Microsoft.FSharp.Collections.SeqModule")
            .GetMembers(System.Reflection.BindingFlags.Static ||| System.Reflection.BindingFlags.Public)
        |> Seq.choose tryGetFunctionName
        |> Seq.toArray

    member this.linqMethods =
        typeof<System.Linq.Enumerable>
            .GetMembers(System.Reflection.BindingFlags.Static ||| System.Reflection.BindingFlags.Public)
        |> Seq.map (fun m -> m.Name)
        |> Seq.distinct
        |> Seq.toArray

    [<TestCaseSource("seqFuncs")>]
    member this.``Seq function has a mapping`` (funcName:string) =
        SeqAndLinq.mappings
        |> Seq.collect (fun m -> m.SeqFuncs)
        |> Seq.map (fun f -> f.Name)
        |> shouldContain funcName

    [<TestCaseSource("linqMethods")>]
    member this.``Linq method has a mapping`` (methodName:string) =
        SeqAndLinq.mappings
        |> Seq.collect (fun m -> m.LinqMethods)
        |> Seq.map (fun f -> f.Name)
        |> shouldContain methodName
