open System

let readInput=
    Seq.initInfinite(ignore >> Console.ReadLine)
    |> Seq.takeWhile(fun line -> line <> null)
    |> Seq.map(
        _.Split(" ", StringSplitOptions.RemoveEmptyEntries)
        >> Array.map int
        >> fun x -> (x.[0], x.[1])
    )

readInput
    |> Seq.toArray
    |> Array.unzip
    |> fun (a1, a2) -> (Array.sort(a1), Array.sort(a2))
    |> fun (a1, a2) -> Array.zip a1 a2
    |> Seq.map (fun (x, y) -> abs (x - y))
    |> Seq.sum
    |> printfn "%A"
