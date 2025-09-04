import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShTaskComponent } from './sh-task.component';

describe('ShTaskComponent', () => {
  let component: ShTaskComponent;
  let fixture: ComponentFixture<ShTaskComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShTaskComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShTaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
