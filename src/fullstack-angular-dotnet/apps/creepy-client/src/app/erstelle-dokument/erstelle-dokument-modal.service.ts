import { HttpClient } from '@angular/common/http';
import { EventEmitter, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { ErzeugeNeuesAngebotDto } from '../models/dokument';
import { catchError, tap, throwError } from 'rxjs';
import { PassDataBetweenComponentsService } from '../services/pass-data-between-components.service';

@Injectable({ providedIn: 'root' })
export class ErstelleDokumentModalService {
    private modal: any;
    public dokument: ErzeugeNeuesAngebotDto | undefined | any;
    public saved = new EventEmitter<void>()

    constructor(private httpClient: HttpClient, private passData: PassDataBetweenComponentsService) {

    }

    set(modal: any) {
        this.modal = modal;
    }

    remove() {
        this.modal = undefined;
    }

    open() {
        this.modal.open();
    }

    async close(save: boolean) {
        this.passData.updateErrorMessageDokumentErstellen("");
        if (save && this.dokument) {
            await this.httpClient.post(environment.baseurl + '/dokumente', { ...this.dokument }).toPromise()
            .then(() => {
                this.saved.emit();
                this.modal.close();
             })
            .catch(e => {
                this.handleError(e);
             })
            //  this.saved.emit();
        }
        if (!save) this.modal.close();
    }

    handleError(err:any) {
        this.passData.updateErrorMessageDokumentErstellen(err.error.error);
    }


}
