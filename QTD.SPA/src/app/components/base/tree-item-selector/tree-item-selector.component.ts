import {
    Component,
    EventEmitter,
    Input,
    OnInit,
    ViewChild,
    SimpleChanges,
    OnChanges
} from '@angular/core';
import {UntypedFormBuilder, UntypedFormControl, UntypedFormGroup} from '@angular/forms';
import {MatLegacySelect as MatSelect} from '@angular/material/legacy-select';
import {TreeItemOptionViewModel} from 'src/app/_DtoModels/TreeVMs/TreeItemOptionViewModel';
import {TreeItemViewModel} from 'src/app/_DtoModels/TreeVMs/TreeItemViewModel';

@Component({
    selector: 'app-tree-item-selector',
    templateUrl: './tree-item-selector.component.html',
    styleUrls: ['./tree-item-selector.component.scss'],
})
export class TreeItemSelectorComponent implements OnInit, OnChanges {
    treeItemForm: UntypedFormGroup;
    @Input() parentDataChangedEventEmitter: EventEmitter<any>;
    @Input() inputTreeItem: TreeItemViewModel;
    @Input() handleTreeItemSelection: (e) => void | undefined;
    selectedTreeOption: TreeItemOptionViewModel;
    selectedTreeOptionChanged = new EventEmitter<any>();
    @ViewChild('selectComponent') selectComponent: MatSelect;
    public treeItemList: any;
    public treeItemList_Orignal: any;

    _handleTreeItemSelection(e) {
        if (
            this.handleTreeItemSelection &&
            typeof this.handleTreeItemSelection === 'function'
        ) {
            this.handleTreeItemSelection(e);
        }
    }

    constructor(private fb: UntypedFormBuilder) {
    }

    ngOnInit(): void {
        this.subscribeToParentDataChange();
        this.initializeForm();
        this.treeItemList = this.inputTreeItem.treeItemOptions;
        this.treeItemList_Orignal = Object.assign(this.treeItemList);
    }

    initializeForm() {
        this.treeItemForm = this.fb.group({
            searchTxt: new UntypedFormControl('')
        });
    }

    ngOnChanges(changes: SimpleChanges) {
        if (changes.inputTreeItem?.currentValue) {
            this.inputTreeItem = changes.inputTreeItem.currentValue;
            this.treeItemList = this.inputTreeItem.treeItemOptions;
            this.treeItemList_Orignal = Object.assign(this.treeItemList);
        }
    }

    openLinkedDataOptions(){
        this.treeItemList = this.treeItemList_Orignal;
    }

    getInputFreeItemSearch() {
        var filterString = this.treeItemForm.get('searchTxt')?.value;
        this.treeItemList = this.treeItemList_Orignal.filter(r => r.display.toLowerCase().startsWith(filterString));
    }

    clearFilter() {
        this.treeItemForm.get('searchTxt')?.setValue('');
        this.treeItemList = this.inputTreeItem.treeItemOptions;
    }

    subscribeToParentDataChange() {
        if (this.parentDataChangedEventEmitter) {
            this.parentDataChangedEventEmitter.subscribe(() => {
                this.parentDataChangedEvent();
            });
        }
    }

    parentDataChangedEvent() {
        this.setSelectedTreeOption(undefined);
        this.selectComponent.value = null;
    }

    treeItemSelected(e: any) {
        this.setSelectedTreeOption(
            this.inputTreeItem.treeItemOptions.find((item) => item.id == e.value)
        );
        if (!this.selectedTreeOption.subTreeItem) {
            this._handleTreeItemSelection(this.selectedTreeOption.id);
        } else {
            this._handleTreeItemSelection(null);
        }
    }

    setSelectedTreeOption(value: TreeItemOptionViewModel) {
        this.selectedTreeOption = value;
        this.selectedTreeOptionChanged.emit();
    }
}
