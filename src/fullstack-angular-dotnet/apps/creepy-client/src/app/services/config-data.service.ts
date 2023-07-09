import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap, map, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from "../../environments/environment";
import { Global } from '../shared/global';
import { ConfigTypeDto } from '../models/config-type-dto';


@Injectable({
  providedIn: 'root'
})
export class ConfigDataService {

  constructor(private http: HttpClient) { }


  holeBerechnungsarten$(): Observable<ConfigTypeDto[]> {
    return this.http
      .get<ConfigTypeDto[]>(environment.baseurl + '/Config/Berechnungsarten')
      .pipe(tap(), catchError(Global.handleError));
  }

  holeDokumententypen$(): Observable<ConfigTypeDto[]> {
    return this.http
      .get<ConfigTypeDto[]>(environment.baseurl + '/Config/Dokumententypen')
      .pipe(tap(), catchError(Global.handleError));
  }

  holeRisikoarten$(): Observable<ConfigTypeDto[]> {
    return this.http
      .get<ConfigTypeDto[]>(environment.baseurl + '/Config/Risikoarten')
      .pipe(tap(), catchError(Global.handleError));
  }

}
