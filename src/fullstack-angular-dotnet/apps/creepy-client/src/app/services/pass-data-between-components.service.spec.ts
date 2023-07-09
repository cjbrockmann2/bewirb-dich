import { TestBed } from '@angular/core/testing';

import { PassDataBetweenComponentsService } from './pass-data-between-components.service';

describe('PassDataBetweenComponentsService', () => {
  let service: PassDataBetweenComponentsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PassDataBetweenComponentsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
