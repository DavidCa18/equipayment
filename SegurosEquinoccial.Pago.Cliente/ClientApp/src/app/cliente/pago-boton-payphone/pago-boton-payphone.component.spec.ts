import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PagoBotonPayphoneComponent } from './pago-boton-payphone.component';

describe('PagoBotonPayphoneComponent', () => {
  let component: PagoBotonPayphoneComponent;
  let fixture: ComponentFixture<PagoBotonPayphoneComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PagoBotonPayphoneComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PagoBotonPayphoneComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
