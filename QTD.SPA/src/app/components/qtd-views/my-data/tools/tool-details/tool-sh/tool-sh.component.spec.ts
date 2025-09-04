import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ToolShComponent } from './tool-sh.component';

describe('ToolShComponent', () => {
  let component: ToolShComponent;
  let fixture: ComponentFixture<ToolShComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ToolShComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ToolShComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
