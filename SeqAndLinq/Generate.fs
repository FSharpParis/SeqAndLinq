module Generate

open SeqAndLinq

let renderNameAndSamples =
    Seq.map (fun f -> sprintf "**%s**<br>Ex: `%s`" f.Name f.Sample)
    >> String.concat "<br>"

let intro = @"
# Mappings between F# Seq and LINQ

This is an attempt do build an exhaustive list of mappings between
F# sequence manipulation functions and LINQ's extension methods on
`IEnumerable<T>`, in order to ease the transition for C# devs.

The current list is absolutely **NOT** complete. Running the
unit tests will tell you which mappings are missing, so please
contribute!
"

let markdown = seq {
    yield intro
    yield "|F# Seq functions|LINQ methods|Explanation|"
    yield "|---|---|---|"
    for mapping in mappings do
        let seqSide = renderNameAndSamples mapping.SeqFuncs
        let linqSide = renderNameAndSamples mapping.LinqMethods
        yield sprintf "|%s|%s|%s|" seqSide linqSide mapping.Explanation
}

let outputFile = System.IO.Path.Combine(__SOURCE_DIRECTORY__, "..", "README.md")
System.IO.File.WriteAllLines(outputFile, markdown)
