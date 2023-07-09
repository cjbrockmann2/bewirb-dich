import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DokumentenListeComponent } from './dokumenten-liste.component';

describe('DokumentenListeComponent', () => {
  let component: DokumentenListeComponent;
  let fixture: ComponentFixture<DokumentenListeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DokumentenListeComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(DokumentenListeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
