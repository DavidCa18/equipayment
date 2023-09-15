import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PagoErrorComponent } from './pago-error.component';

describe('PagoErrorComponent', () => {
  let component: PagoErrorComponent;
  let fixture: ComponentFixture<PagoErrorComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PagoErrorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PagoErrorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
