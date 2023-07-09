import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
 

@Component({
  selector: 'fullstack-angular-dotnet-generische-tabelle',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './generische-tabelle.component.html',
  styleUrls: ['./generische-tabelle.component.scss'],
})
export class GenerischeTabelleComponent implements OnInit {

  @Input() titel = '' ;
  @Input() tabellenSpalten: TabellenSpalten[] = [];
  @Input() tabellenDaten: any[] = [] ;
  spaltenNamen: string[] = [];


  ngOnInit(): void {

    this.tabellenSpalten.forEach(element => {
      this.spaltenNamen.push(element.columnName);
    });
  }
  


}


export class TabellenSpalten {
  columnName = '';
  columnTitle = '';
  columnFormat: 'string' | 'numeric' | 'currency' | 'boolean' =  'string';
}

