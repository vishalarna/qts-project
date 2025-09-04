import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ToolTaskComponent } from './tool-task.component';

describe('ToolTaskComponent', () => {
  let component: ToolTaskComponent;
  let fixture: ComponentFixture<ToolTaskComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ToolTaskComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ToolTaskComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
