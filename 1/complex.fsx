open System
open System.Collections.Generic

let readInput=
    Seq.initInfinite(ignore >> Console.ReadLine)
    |> Seq.takeWhile(fun line -> line <> null)
    |> Seq.map(
        _.Split(" ", StringSplitOptions.RemoveEmptyEntries)
        >> Array.map int
        >> fun x -> (x.[0], x.[1])
    )

let count s =
    let ret = Dictionary<int, int>()
    Seq.iter(fun x ->
        ret.TryAdd(x, 0) |> ignore
        ret[x] <- ret[x] + 1
    ) s
    ret

readInput
    |> Seq.toArray
    |> Array.unzip
    |> fun (a1, a2) -> (
        let counts = count a2
        a1
        |> Seq.map (fun elem ->
            if counts.ContainsKey elem then elem * counts.[elem] else 0 
        )
        |> Seq.sum
    )
    |> printfn "%A"
