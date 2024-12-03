open System

let readInput=
    Seq.initInfinite(ignore >> Console.ReadLine)
    |> Seq.takeWhile(fun line -> line <> null)
    |> Seq.map(
        _.Split(" ", StringSplitOptions.RemoveEmptyEntries)
        >> Array.map int
    )

readInput
    |> Seq.map (fun line ->
        let incr = line.[0] < line.[1]
        line
        |> Seq.windowed 2
        |> Seq.map(fun x -> (x.[0], x.[1]))
        |> Seq.exists(
            fun (prev, next) ->
                let warn1 = if incr then next <= prev else next >= prev
                let warn2 = abs (next - prev) < 1 || abs (next - prev) > 3
                warn1 || warn2
        )
        |> fun x -> if x then 0 else 1
    )
    |> Seq.sum
    |> printfn "%A"
