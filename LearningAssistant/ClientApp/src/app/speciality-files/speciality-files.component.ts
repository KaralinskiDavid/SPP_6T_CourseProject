import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { FileSectionService } from '../services/file-section.service';
import { FileService } from '../services/file.service';
import { SpecialityFileSection, SpecialityFile, Group } from '../classes/iismodels';
import { GroupService } from '../services/group.service';
import { AddSectionModalComponent } from '../add-section-modal/add-section-modal.component';

@Component({
  selector: 'app-speciality-files',
  templateUrl: './speciality-files.component.html',
  styleUrls: ['./speciality-files.component.css']
})
export class SpecialityFilesComponent implements OnInit {

  sections: SpecialityFileSection[];
  currentSpecialityId: number;
  canEdit = false;

  ALLOW_FILE_EXT = '.pdf, .jpeg, .jpg, .png, .doc, .docx, .xls, .xlsx';
  ALLOW_FILE_SIZE = 10000000;

  constructor(private toastr: ToastrService, private _modalService: NgbModal, private _fileSectionService: FileSectionService, private _fileService: FileService, private _groupService: GroupService) { }

  ngOnInit() {
    this.canEdit = localStorage.getItem('role') != 'SpecialityHeadman';
    let groupNumber = localStorage.getItem('groupNumber');
    this._groupService.getGroupByNumber(groupNumber).subscribe((result: Group) => {
      this.currentSpecialityId = result.specialityId;
      this.loadSections();
    });
  }

  loadSections() {
    this._fileSectionService.getFileSections(this.currentSpecialityId).subscribe((result: SpecialityFileSection[]) => {
      this.sections = result;
    });
  }

  onCreateClicked() {
    const modalRef = this._modalService.open(AddSectionModalComponent);
    modalRef.result.then((result) => {
      if (result) {
        let fs = new SpecialityFileSection();
        fs.name = result;
        fs.specialityId = this.currentSpecialityId;
        this._fileSectionService.createFileSection(fs).subscribe((result: boolean) => {
          if (result) {
            this.toastr.success("Created");
            this.loadSections();
          }
          else
            this.toastr.error("Something went wrong");
        },
          error => { this.toastr.error("Something went wrong"); }
        )
      };
    }).catch((res) => { });
  }

  onFileSelect(event, index: number) {

    console.log(event);
    event.addedFiles.forEach((item, itemIndex) => {
      let itemName = item.name;
      if (event.addedFiles.find((function (value, index) {
        return value.name == itemName && index != itemIndex;
      })))
        itemName = "1" + itemName;
      if (this.sections[index].specialityFiles != null && this.sections[index].specialityFiles.find((function (value) {
        return value.name == itemName;
      })))
        itemName = "1" + itemName;
      const formData = new FormData();
      formData.append("uploadedFile", item);
      this._fileService.uploadFile(formData, itemName, this.sections[index].id).subscribe((response: any) => {
        this.sections[index].specialityFiles.push(response);
        console.log(response);
        this.toastr.success('File saved');
      },
        error => {
          console.log(error);
          this.toastr.error('Something went wrong');
        }
      );
    });
  }

  onFileRemove(event : SpecialityFile, index: number) {
    console.log(event);

    if (event.id) {
      this._fileService.deleteFile(event.id).subscribe((response: any) => {
        console.log(response);
        this.sections[index].specialityFiles = this.sections[index].specialityFiles.filter(function (value) {
          return value.name != event.name;
        }
        );
        this.toastr.success('File removed');
      },
        error => {
          console.log(error);
          this.toastr.error('Something went wrong');
        }
      );
    }
  }

  onFileItemClick(file: SpecialityFile) {
    event.preventDefault();
    event.stopPropagation();
    this.downloadDocument(file);
  }

  downloadDocument(file: SpecialityFile) {

    this._fileService.downloadFile(file.id).subscribe((response: any) => {
      if (response.byteLength > 0) {
        let a = document.createElement("a");
        document.body.appendChild(a);
        //a.style = "display: none";
        let blob = new Blob([response]);
        let url = URL.createObjectURL(blob);
        //window.open(fileURL);
        a.href = url;
        a.download = file.name;
        a.click();
        //window.URL.revokeObjectURL(url);
      } else {
        alert('Что-то пошло не так');
      }
      console.log(response);
    },
      error => {
        console.log(error);
        alert('Что-то пошло не так');
      }
    );
  }

}
