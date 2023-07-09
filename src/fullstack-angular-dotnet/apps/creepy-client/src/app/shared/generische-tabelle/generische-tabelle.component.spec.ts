import { ComponentFixture, TestBed } from '@angular/core/testing';
import { GenerischeTabelleComponent } from './generische-tabelle.component';

describe('GenerischeTabelleComponent', () => {
  let component: GenerischeTabelleComponent;
  let fixture: ComponentFixture<GenerischeTabelleComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [GenerischeTabelleComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(GenerischeTabelleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
