/**
    Make it "less" likely with straightsegments when there are many in a row!
    Make corridors scalable
    Avancerade större rum = eget segment med random? Kanske kolla om man är främst i GlobalDirection, och i så fall aktivera möjligheten?
    Segmented room - several boxes!

    Skicka med om det är "Decidenextsegment" till rumsskapande så man slipper räkna på utgångar osv (svårt - logik med utseende integrerat med exits)

    ## Markera scanningsområden i map så att det går att kolla om andra scannar samma områden.. Resetta mellan varje workingSet - blev inte bra!
    --Bugg när "tarmar" ibland inte verkar backas ur?
    --Bugg med att LevelMap inte uppdateras i ett område runt starten
    --Bugg när detection inte verkar funka och korridorer kommer för nära andra
    --Inte backa ur "rum"
    -- Ta bort oavslutade "tarmar" - simpla segment som inte slutar i nånting då de mötte hinder kanske kan rullas tillbaka? Kräver länkad lista!
    -- Göra en koll på straightsegment om man har nånting rakt framöver - och i så fall random, ganska liten - typ 10%(?) chans att ansluta dit
    -- Rumboxar med variabla sidor?
    -- Skicka med antal trådar till rum så man kan anpassa mängden utgångar - dvs med 1 tråd så ska det inte bli 0 utgångar så tråden stoppar. Med 2 trådar kanske chansen att det blir 0 utgångar ska vara mindre?!
    
*/
