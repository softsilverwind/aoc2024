open System

let readInput=
    Seq.initInfinite(ignore >> Console.ReadLine)
    |> Seq.takeWhile(fun line -> line <> null)
    |> Seq.map(
        _.Split(" ", StringSplitOptions.RemoveEmptyEntries)
        >> Array.map int
        >> Array.toList
    )

let problem incr (prev, next) =
    let warn1 = if incr then next <= prev else next >= prev
    let warn2 = abs (next - prev) < 1 || abs (next - prev) > 3
    warn1 || warn2

let rec rectify incr list =
    match list with
        [] -> ([], [])
        | [h] -> ([h], [])
        | (prev::next::tail) ->
            match rectify incr (next::tail) with
                (_, []) when problem incr (prev, next) -> (prev::tail, next::tail)
                | (_, []) -> (prev::next::tail, [])
                | (l1, l2) -> (prev::l1, prev::l2)

let check list =
    match list with
        (h1::h2::tail) ->
            let incr = h1 < h2
            (h1::h2::tail)
            |> Seq.windowed 2
            |> Seq.map(fun x -> (x.[0], x.[1]))
            |> Seq.exists(problem incr)
        | _ -> false

readInput
    |> Seq.map (fun line ->
        let (l1, l2) = rectify true line
        let (l3, l4) = rectify false line
        seq [l1; l2; l3; l4]
            |> Seq.exists (check >> not)
            |> fun x -> if x then 1 else 0
    )
    |> Seq.sum
    |> printfn "%A"
