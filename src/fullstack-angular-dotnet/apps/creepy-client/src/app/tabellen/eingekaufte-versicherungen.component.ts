import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TabellenSpalten } from '../shared/generische-tabelle/generische-tabelle.component';
import { PassDataBetweenComponentsService } from '../services/pass-data-between-components.service';
import { DokumentenlisteEintragDto } from '../models/dokument';
import { GenerischeTabelleComponent } from '../shared/generische-tabelle/generische-tabelle.component';


/*************************************************************************************
 * Mit dieser Komponente mache ich mir der allgemeinen generischen Tabelle zunutze....
 *************************************************************************************/

@Component({
  selector: 'fullstack-angular-dotnet-eingekaufte-versicherungen',
  standalone: true,
  imports: [CommonModule, GenerischeTabelleComponent],
  template: `
      <fullstack-angular-dotnet-generische-tabelle
          titel="Versendete Versicherungsscheine" 
          [tabellenSpalten]="spalten"
          [tabellenDaten]="tabellendaten">
      </fullstack-angular-dotnet-generische-tabelle>`,
  styles: [],
})
export class EingekaufteVersicherungenComponent implements OnInit {

  spalten: TabellenSpalten[] = [];
  tabellendaten: any[] = [] ;

  constructor(private passData: PassDataBetweenComponentsService) {
    this.spalten = [
      { columnName: 'dokumenttyp', columnTitle:'Typ', columnFormat: 'string' } ,
      { columnName: 'berechnungsart', columnTitle:'Berechnungsart', columnFormat: 'string' } ,
      { columnName: 'zusatzschutz', columnTitle:'Zusatzschutz', columnFormat: 'string' } ,
      { columnName: 'webshopVersichert', columnTitle:'Webshop versichert', columnFormat: 'boolean' } ,
      { columnName: 'risiko', columnTitle:'Risiko', columnFormat: 'string' } ,
      { columnName: 'versicherungssumme', columnTitle:'Versicherungssumme', columnFormat: 'currency' } ,
      { columnName: 'beitrag', columnTitle:'Beitrag', columnFormat: 'currency' } ,
    ];
  }
  ngOnInit(): void {
    this.passData.dokumentenListe$.subscribe((data: DokumentenlisteEintragDto[]) => {
      this.tabellendaten = data.filter(x => x.kannAngenommenWerden == false && x.kannAusgestelltWerden == false) ;
    });
  }


 

}


/*

<table class="tab">

  <tr>
    <th>Typ</th>
    <th>Berechnungsart</th>
    <th>Zusatzschutz</th>
    <th>Webshop versichert</th>
    <th>Risiko</th>
    <th>Versicherungssumme</th>
    <th class="minimumWeite">Beitrag</th>
  </tr>
  <tr [class.selected]="selectedDocument && selectedDocument.id === dokument.id" (click)="selectDocument(dokument)" *ngFor="let dokument of dokumente">
    <td>{{dokument.dokumenttyp}}</td>
    <td>{{dokument.berechnungsart}}</td>
    <td>{{dokument.zusatzschutz}}</td>
    <td>{{dokument.webshopVersichert ? 'ja' : 'nein'}}</td>
    <td>{{dokument.risiko}}</td>
    <td class="rechts">{{dokument.versicherungssumme | currency }} €</td>
    <td class="rechts">{{dokument.beitrag | currency }} €</td>
  </tr>
</table>

*/
