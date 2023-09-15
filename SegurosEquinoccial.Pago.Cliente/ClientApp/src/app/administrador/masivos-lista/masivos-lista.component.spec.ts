import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MasivosListaComponent } from './masivos-lista.component';

describe('MasivosListaComponent', () => {
  let component: MasivosListaComponent;
  let fixture: ComponentFixture<MasivosListaComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MasivosListaComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MasivosListaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
