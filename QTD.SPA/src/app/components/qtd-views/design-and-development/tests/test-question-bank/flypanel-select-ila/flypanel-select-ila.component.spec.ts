import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelSelectIlaComponent } from './flypanel-select-ila.component';

describe('FlypanelSelectIlaComponent', () => {
  let component: FlypanelSelectIlaComponent;
  let fixture: ComponentFixture<FlypanelSelectIlaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelSelectIlaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelSelectIlaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
