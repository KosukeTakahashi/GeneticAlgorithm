namespace LibGeneticAlgorithm

open System

type GeneticAlgorithm(initColor: int, targetColor: int) = 
    let mutable currentColor: int = initColor

    let add a b = a + b
    let random = System.Random ()
    
    let nearColors color =
        let cr = (color >>> 16) &&& 0xFF
        let cg = (color >>> 8)  &&& 0xFF
        let cb = color          &&& 0xFF
        
        let difR, difG, difB = random.Next 2, random.Next 2, random.Next 2

        if (cr = 0) || (cb = 0) || (cg = 0) then
            let plus1  = ((cr + difR) <<< 16) + ((cg + difG) <<< 8) + (cb + difB)
            [plus1]
        else if (cr = 0xFF) || (cg = 0xFF) || (cb = 0xFF) then    
            let minus1 = ((cr - difR) <<< 16) + ((cg - difG) <<< 8) + (cb - difB)
            [minus1]
        else
            let plus1  = ((cr + difR) <<< 16) + ((cg + difG) <<< 8) + (cb + difB)
            let minus1 = ((cr - difR) <<< 16) + ((cg - difG) <<< 8) + (cb - difB)
            [plus1; minus1]
        
    
    let distance color target = 
        let r = ((target >>> 16) &&& 0xFF) - ((color >>> 16) &&& 0xFF)
        let g = ((target >>> 8) &&& 0xFF) - ((color >>> 8) &&& 0xFF)
        let b = (target &&& 0xFF) - (color &&& 0xFF)

        let sq = r * r + g * g + b * b
        Math.Sqrt <| float(sq)

    let minimam (lst: (float * int) list) = 
        let mutable minimum = lst.[0]
        for elem in lst do
            minimum <- min minimum elem
        minimum

    member this.InitColor = initColor
    member this.TargetColor = targetColor

    member this.NextGeneration () =
        let nears = nearColors currentColor
        let distances = ref []
        for near in nears do
            distances := !distances @ [(distance near targetColor, near)]
        
        let m = minimam !distances
        currentColor <- snd m
        m
 
