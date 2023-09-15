import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MasivosGestionComponent } from './masivos-gestion.component';

describe('MasivosGestionComponent', () => {
  let component: MasivosGestionComponent;
  let fixture: ComponentFixture<MasivosGestionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MasivosGestionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MasivosGestionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
