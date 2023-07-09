import { TestBed } from '@angular/core/testing';

import { DokumentenListeService } from './dokumenten-liste.service';

describe('DokumentenListeService', () => {
  let service: DokumentenListeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DokumentenListeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
