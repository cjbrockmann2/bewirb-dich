import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DokumentenlisteEintragDto } from '../models/dokument';
import { ErstelleDokumentModalService } from '../erstelle-dokument/erstelle-dokument-modal.service';
import { tap } from 'rxjs';
import { ErstelleDokumentModalComponent } from '../erstelle-dokument/erstelle-dokument-modal.component';
import { DokumentenListeService } from './dokumenten-liste.service';
import { PassDataBetweenComponentsService } from '../services/pass-data-between-components.service';

@Component({
  selector: 'fullstack-angular-dotnet-dokumenten-liste',
  standalone: true,
  imports: [CommonModule, ErstelleDokumentModalComponent],
  templateUrl: './dokumenten-liste.component.html',
  styleUrls: ['./dokumenten-liste.component.scss'],
})
export class DokumentenListeComponent {


  dokumente: DokumentenlisteEintragDto[] = [];

  selectedDocument: DokumentenlisteEintragDto | undefined

  constructor(
          private eDMService: ErstelleDokumentModalService, 
          private dokumentenService: DokumentenListeService, 
          private passData: PassDataBetweenComponentsService ) {
    eDMService.saved.pipe(tap(() => {
      this.ladeDokumente()
    })).subscribe()
    this.ladeDokumente()
  }

  openDocumentCreation() {
    this.eDMService.open();
  }

  selectDocument(dokument: DokumentenlisteEintragDto) {
    this.selectedDocument = dokument;
  }


  ladeDokumente() {
    this.dokumentenService.holeDokumente$().subscribe({
      next: result => {
        this.passData.updateDokumentenliste(result);
        this.dokumente = result.filter(x => !(x.kannAngenommenWerden == false && x.kannAusgestelltWerden == false));
        }
    });
  }

  selectedDocumentAnnehmen() {
    if(this.selectedDocument) {
      this.dokumentenService.nehmeDokumentAn$(this.selectedDocument.id).subscribe({
        next: () => {
          this.selectedDocument = undefined; 
          this.ladeDokumente();
        }
      });
    }
  }

  selectedDocumentAusstellen() {
    if(this.selectedDocument) {
      this.dokumentenService.stelleDokumentAus$(this.selectedDocument.id).subscribe({
        next: () => {
          this.selectedDocument = undefined; 
          this.ladeDokumente();
        }
      });
    } 
  }

}
