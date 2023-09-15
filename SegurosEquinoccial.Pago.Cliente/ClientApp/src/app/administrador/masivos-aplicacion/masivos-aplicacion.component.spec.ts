import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MasivosAplicacionComponent } from './masivos-aplicacion.component';

describe('MasivosAplicacionComponent', () => {
  let component: MasivosAplicacionComponent;
  let fixture: ComponentFixture<MasivosAplicacionComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MasivosAplicacionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MasivosAplicacionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
