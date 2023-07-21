import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap, map, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { DokumentenlisteEintragDto } from '../models/dokument';
import { environment } from "../../environments/environment";
import { Global } from '../shared/global';

@Injectable({
  providedIn: 'root'
})
export class DokumentenListeService {

  constructor(private http: HttpClient) { }


  holeDokumente$(): Observable<DokumentenlisteEintragDto[]> {
    return this.http
      .get<DokumentenlisteEintragDto[]>(environment.baseurl + '/dokumente')
      .pipe(catchError(Global.handleError));
  }

  nehmeDokumentAn$(id: string): Observable<void> {
    return this.http.post<void>(environment.baseurl + '/dokumente/' + id + '/annehmen',null)
    .pipe(tap(), catchError(Global.handleError));
  } 

  stelleDokumentAus$(id: string): Observable<void> {
    return this.http.post<void>(environment.baseurl + '/dokumente/' + id + '/ausstellen',null)
    .pipe(tap(), catchError(Global.handleError));
  } 



}
