import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MasivosComponent } from './masivos.component';

describe('MasivosComponent', () => {
  let component: MasivosComponent;
  let fixture: ComponentFixture<MasivosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MasivosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MasivosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
