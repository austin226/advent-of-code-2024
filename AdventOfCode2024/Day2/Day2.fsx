// https://adventofcode.com/2024/day/2

module Common =
    open System.IO

    let input fileName =
        let dir = __SOURCE_DIRECTORY__
        let inFile = $"{dir}/{fileName}"
        let fileContents = File.ReadAllLines inFile

        fileContents

module Part1 =
    let safe levels =
        let allPairs condition array =
            array |> Array.pairwise |> Array.map condition |> Array.forall id

        let monotonic levels =
            let allIncreasing = allPairs (fun p -> fst p <= snd p)
            let allDecreasing = allPairs (fun p -> fst p >= snd p)
            allIncreasing levels || allDecreasing levels

        let allGradual =
            let gradualChange pair =
                let diff = abs (fst pair - snd pair)
                diff >= 1 && diff <= 3

            allPairs gradualChange

        monotonic levels && allGradual levels

    let solve fileName =
        let parseReport line = line |> Array.map int

        let reports =
            Common.input fileName |> Array.map _.Split(" ") |> Array.map parseReport

        reports |> Array.filter safe |> Array.length


    let exampleResult = solve "example_input.txt"
    let fullResult = solve "input.txt"
