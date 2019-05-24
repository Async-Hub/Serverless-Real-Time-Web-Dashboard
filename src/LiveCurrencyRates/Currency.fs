namespace LiveCurrencyRates

module Currency =
    type ExchangeRate = { Type : string; Rate : float }
    type UnitsPerUsd = {Value: float}
    type Currency  = { Currency : string; RatePerUsd : UnitsPerUsd }

    let GetNewRates () = 
        let createERate (t,r) = { Type = t; Rate = r }
        let createCurrency (c,r) = { Currency = c; RatePerUsd = r }

        let usdRateName = "USD"

        let baseUnitsPerUSD = 
            [createCurrency ("AUD", {Value = 1.4541985301})
             createCurrency ("CAD", {Value = 1.3465198866})
             createCurrency ("CNY", {Value = 6.9182116184})
             createCurrency ("EUR", {Value = 0.8979108768})
             createCurrency ("GBP", {Value = 0.7909851152})
             createCurrency ("JPY", {Value = 110.0996639301})
             createCurrency ("RUB", {Value = 64.6965192253})]

        let rnd = fun () -> (System.Random().Next(-100, 100) |> float) * 0.005
        let round = fun (f:float) -> System.Math.Round(f, 8)

        let generateNewRate (cur:Currency) = 
            let newRate = cur.RatePerUsd.Value + rnd() |> round
            let exchangeType = sprintf "%s / %s" cur.Currency usdRateName
        
            {Type = exchangeType ; Rate = newRate}

        baseUnitsPerUSD |> List.map generateNewRate 
