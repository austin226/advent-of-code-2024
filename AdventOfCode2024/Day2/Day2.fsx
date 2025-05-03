// https://adventofcode.com/2024/day/2

module Common =
    open System.IO

    let input fileName =
        let dir = __SOURCE_DIRECTORY__
        let inFile = $"{dir}/{fileName}"
        let fileContents = File.ReadAllLines inFile

        fileContents

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

module Part1 =
    let solve fileName =
        let parseReport line = line |> Array.map int

        let reports =
            Common.input fileName |> Array.map _.Split(" ") |> Array.map parseReport

        reports |> Array.filter Common.safe |> Array.length


    let exampleResult = solve "example_input.txt"
    let fullResult = solve "input.txt"

module Part2 =
    let rmIdx (report: int array) i =
        report
        |> Array.mapi (fun j x -> if i <> j then Some x else None)
        |> Array.choose id

    let alts (report: int array) = Seq.init report.Length (rmIdx report)

    let almostSafe (report: int array) = alts report |> Seq.exists Common.safe

    let solve fileName =
        let parseReport line = line |> Array.map int

        let reports =
            Common.input fileName |> Array.map _.Split(" ") |> Array.map parseReport

        reports |> Array.filter almostSafe |> Array.length

    let exampleResult = solve "example_input.txt"
    let fullResult = solve "input.txt"
