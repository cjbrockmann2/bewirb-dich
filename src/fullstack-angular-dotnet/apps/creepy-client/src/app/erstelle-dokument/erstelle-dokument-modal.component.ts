import { Component,  ElementRef, OnInit, OnDestroy } from '@angular/core';
import { ErstelleDokumentModalService } from './erstelle-dokument-modal.service';
import {FormsModule} from "@angular/forms";
import {CommonModule} from "@angular/common";
import { PassDataBetweenComponentsService } from '../services/pass-data-between-components.service';
import { ConfigTypeDto } from '../models/config-type-dto';

@Component({
    selector: 'fullstack-angular-dotnet-dokument-modal',
    templateUrl: 'erstelle-dokument-modal.component.html',
    styleUrls: ['erstelle-dokument-modal.component.scss'],
    imports: [CommonModule, FormsModule],
    standalone: true
})
export class ErstelleDokumentModalComponent implements OnInit, OnDestroy {

    private element: any;

    berechnungsArten: ConfigTypeDto[] = [];
    risikoTypen: ConfigTypeDto[] = [];
    errorMessage = ''; 

    constructor(
                public erstelleDokumentModalService: ErstelleDokumentModalService
                , private el: ElementRef
                , private passData: PassDataBetweenComponentsService
    ) {
        this.element = el.nativeElement;
    }

    ngOnInit(): void {

        document.body.appendChild(this.element);

        this.element.addEventListener('click', (el: any )=> {
            if (el.target.className === 'modal') {
                this.close();
            }
        });

        this.passData.berechnungsArten$.subscribe(x => {this.berechnungsArten = x;});
        this.passData.risikoArten$.subscribe(x => {this.risikoTypen = x;});
        this.passData.errorMessageDokumentErstellen$.subscribe(x => {this.errorMessage = x});
        this.erstelleDokumentModalService.set(this);
    }

    ngOnDestroy(): void {
        this.erstelleDokumentModalService.remove();
        this.element.remove();
    }

    open(): void {
        this.erstelleDokumentModalService.dokument = {}
        this.element.style.display = 'block';
        document.body.classList.add('modal-open');
    }

    close(): void {
        this.element.style.display = 'none';
        document.body.classList.remove('modal-open');
    }

}
