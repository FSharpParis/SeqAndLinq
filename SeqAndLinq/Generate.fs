module Generate

open SeqAndLinq

let renderNameAndSample nameAndSample =
    sprintf "**%s**<br>Ex: `%s`" nameAndSample.Name nameAndSample.Sample

let intro = @"
# Mappings between F# Seq and LINQ

This is an attempt do build an exhaustive list of mappings between
LINQ's extension methods on `IEnumerable<T>` and F# sequence manipulation
functions, in order to ease the transition for C# devs.

The current list is absolutely **NOT** complete. Running the
unit tests will tell you which mappings are missing, so please
contribute!
"

let markdown = seq {
    yield intro
    yield "|LINQ methods|F# Seq functions|Explanation|"
    yield "|---|---|---|"
    for mapping in mappings do
        let linqSide = renderNameAndSample mapping.LinqMethod
        let seqSide = renderNameAndSample mapping.SeqFunc
        yield sprintf "|%s|%s|%s|" linqSide seqSide mapping.Explanation
}

let outputFile = System.IO.Path.Combine(__SOURCE_DIRECTORY__, "..", "README.md")
System.IO.File.WriteAllLines(outputFile, markdown)
