import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProcedureOverviewComponent } from './procedure-overview.component';

describe('ProcedureOverviewComponent', () => {
  let component: ProcedureOverviewComponent;
  let fixture: ComponentFixture<ProcedureOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProcedureOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProcedureOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
