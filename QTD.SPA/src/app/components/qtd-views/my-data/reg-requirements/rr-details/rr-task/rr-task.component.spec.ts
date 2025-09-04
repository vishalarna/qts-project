import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RRTaskComponent } from './rr-task.component';

describe('RRTaskComponent', () => {
  let component: RRTaskComponent;
  let fixture: ComponentFixture<RRTaskComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RRTaskComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RRTaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
