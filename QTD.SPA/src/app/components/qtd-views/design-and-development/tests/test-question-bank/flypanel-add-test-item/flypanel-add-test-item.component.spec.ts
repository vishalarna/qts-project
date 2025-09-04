import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelAddTestItemComponent } from './flypanel-add-test-item.component';

describe('FlypanelAddTestItemComponent', () => {
  let component: FlypanelAddTestItemComponent;
  let fixture: ComponentFixture<FlypanelAddTestItemComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelAddTestItemComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelAddTestItemComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
