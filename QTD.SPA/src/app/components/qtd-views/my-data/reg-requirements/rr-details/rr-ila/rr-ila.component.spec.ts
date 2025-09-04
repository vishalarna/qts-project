import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RRIlaComponent } from './rr-ila.component';

describe('RRIlaComponent', () => {
  let component: RRIlaComponent;
  let fixture: ComponentFixture<RRIlaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RRIlaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RRIlaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
