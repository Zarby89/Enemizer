import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnemizerFormComponent } from './enemizer-form.component';

describe('EnemizerFormComponent', () => {
  let component: EnemizerFormComponent;
  let fixture: ComponentFixture<EnemizerFormComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EnemizerFormComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnemizerFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
