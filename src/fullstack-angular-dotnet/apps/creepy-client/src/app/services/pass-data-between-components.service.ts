import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { DokumentenlisteEintragDto } from '../models/dokument';
import { ConfigTypeDto } from '../models/config-type-dto';
import { ErrorMessageDokumentErstellen } from '../models/error-message-dokument-erstellen';

@Injectable({
  providedIn: 'root'
})
export class PassDataBetweenComponentsService {

  private dokumentenlisteSource : BehaviorSubject<DokumentenlisteEintragDto[]> = new BehaviorSubject<DokumentenlisteEintragDto[]>([] as DokumentenlisteEintragDto[]);
  dokumentenListe$ = this.dokumentenlisteSource.asObservable();

  private berechnungsArtenSource : BehaviorSubject<ConfigTypeDto[]> = new BehaviorSubject<ConfigTypeDto[]>([] as ConfigTypeDto[]);
  berechnungsArten$ = this.berechnungsArtenSource.asObservable();

  private dokumentenTypenSource : BehaviorSubject<ConfigTypeDto[]> = new BehaviorSubject<ConfigTypeDto[]>([] as ConfigTypeDto[]);
  dokumentenTypen$ = this.dokumentenTypenSource.asObservable();

  private risikoArtenSource : BehaviorSubject<ConfigTypeDto[]> = new BehaviorSubject<ConfigTypeDto[]>([] as ConfigTypeDto[]);
  risikoArten$ = this.risikoArtenSource.asObservable();

  private errorMessageDokumentErstellenSource : BehaviorSubject<string> = new BehaviorSubject<string>('');
  errorMessageDokumentErstellen$ = this.errorMessageDokumentErstellenSource.asObservable();


  // constructor() {  }

  public updateDokumentenliste(data: DokumentenlisteEintragDto[]) {
    this.dokumentenlisteSource.next(data);
  }

  public updateBerechnungsarten(data: ConfigTypeDto[]) {
    this.berechnungsArtenSource.next(data);
  }

  public updateDokumententypen(data: ConfigTypeDto[]) {
    this.dokumentenTypenSource.next(data);
  }

  public updateRisikoArtenSource(data: ConfigTypeDto[]) {
    this.risikoArtenSource.next(data);
  }

  public updateErrorMessageDokumentErstellen(data: string) {
    this.errorMessageDokumentErstellenSource.next(data);
    setTimeout(() => this.errorMessageDokumentErstellenSource.next(""),  6000);
  }


}
