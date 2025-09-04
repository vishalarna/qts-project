import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FlypanelChangeIlaComponent } from './flypanel-change-ila.component';

describe('FlypanelChangeIlaComponent', () => {
  let component: FlypanelChangeIlaComponent;
  let fixture: ComponentFixture<FlypanelChangeIlaComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FlypanelChangeIlaComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FlypanelChangeIlaComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
