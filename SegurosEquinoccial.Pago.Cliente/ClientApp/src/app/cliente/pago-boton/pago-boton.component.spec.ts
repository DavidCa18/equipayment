import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PagoBotonDatafastComponent } from './pago-boton.component';

describe('PagoBotonDatafastComponent', () => {
  let component: PagoBotonDatafastComponent;
  let fixture: ComponentFixture<PagoBotonDatafastComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [PagoBotonDatafastComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PagoBotonDatafastComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
