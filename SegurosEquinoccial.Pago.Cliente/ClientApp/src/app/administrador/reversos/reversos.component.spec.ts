import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ReversosComponent } from './reversos.component';

describe('ReversosComponent', () => {
  let component: ReversosComponent;
  let fixture: ComponentFixture<ReversosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ReversosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ReversosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
