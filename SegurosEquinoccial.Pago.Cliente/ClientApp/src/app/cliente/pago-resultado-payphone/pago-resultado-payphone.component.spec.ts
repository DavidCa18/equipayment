import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PagoResultadoPayphoneComponent } from './pago-resultado-payphone.component';

describe('PagoResultadoPayphoneComponent', () => {
  let component: PagoResultadoPayphoneComponent;
  let fixture: ComponentFixture<PagoResultadoPayphoneComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PagoResultadoPayphoneComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PagoResultadoPayphoneComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
