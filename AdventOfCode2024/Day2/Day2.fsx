// https://adventofcode.com/2024/day/2

module Common =
    open System.IO

    let input fileName =
        File.ReadAllLines $"{__SOURCE_DIRECTORY__}/{fileName}"

    let safe report =
        let allPairs condition =
            report |> Array.pairwise |> Array.map condition |> Array.forall id

        let monotonic =
            let allIncreasing = allPairs (fun p -> fst p <= snd p)
            let allDecreasing = allPairs (fun p -> fst p >= snd p)
            allIncreasing || allDecreasing

        let allGradual =
            let gradualChange pair =
                let diff = abs (fst pair - snd pair)
                diff >= 1 && diff <= 3

            allPairs gradualChange

        monotonic && allGradual

    let solve fileName condition =
        let parseReport line = line |> Array.map int
        let reports = input fileName |> Array.map _.Split(" ") |> Array.map parseReport
        reports |> Array.filter condition |> Array.length

module Part1 =
    open Common

    let exampleResult = solve "example_input.txt" safe
    let fullResult = solve "input.txt" safe

module Part2 =
    open Common

    let rmIdx report i =
        report
        |> Array.mapi (fun j x -> if i <> j then Some x else None)
        |> Array.choose id

    let alts (report: 'a array) = Seq.init report.Length (rmIdx report)
    let almostSafe = alts >> Seq.exists safe

    let exampleResult = solve "example_input.txt" almostSafe
    let fullResult = solve "input.txt" almostSafe
