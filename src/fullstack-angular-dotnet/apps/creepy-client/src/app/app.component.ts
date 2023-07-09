import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DokumentenListeComponent } from './dokumenten-liste/dokumenten-liste.component';
import { EingekaufteVersicherungenComponent } from './tabellen/eingekaufte-versicherungen.component';
import { ConfigDataService } from './services/config-data.service';
import { PassDataBetweenComponentsService } from './services/pass-data-between-components.service';

@Component({
  selector: 'fullstack-angular-dotnet-root',
  standalone: true,
  imports: [CommonModule, DokumentenListeComponent, EingekaufteVersicherungenComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit  {

  constructor(private configData: ConfigDataService, private passData: PassDataBetweenComponentsService) {

  }
  ngOnInit(): void {
    this.configData.holeBerechnungsarten$().subscribe(data => {
      this.passData.updateBerechnungsarten(data);
    });

    this.configData.holeDokumententypen$().subscribe(data => {
      this.passData.updateDokumententypen(data);
    });

    this.configData.holeRisikoarten$().subscribe(data => {
      this.passData.updateRisikoArtenSource (data);
    });


  }


}
