import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PagoResultadoComponent } from './pago-resultado.component';

describe('PagoResultadoComponent', () => {
  let component: PagoResultadoComponent;
  let fixture: ComponentFixture<PagoResultadoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PagoResultadoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PagoResultadoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
