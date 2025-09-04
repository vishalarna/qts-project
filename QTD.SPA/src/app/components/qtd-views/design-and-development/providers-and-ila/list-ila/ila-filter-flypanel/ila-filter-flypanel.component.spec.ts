import { ComponentFixture, TestBed } from '@angular/core/testing';

import { IlaFilterFlypanelComponent } from './ila-filter-flypanel.component';

describe('IlaFilterFlypanelComponent', () => {
  let component: IlaFilterFlypanelComponent;
  let fixture: ComponentFixture<IlaFilterFlypanelComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ IlaFilterFlypanelComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(IlaFilterFlypanelComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
