export type Student = {
    Id: string;
    Name: string;
    Email: string;
    StudentId: string;
    NationalIdValue: string;
};

export type CreateStudentRequest = {
    Name: string;
    Email: string;
    StudentId: string;
    NationalIdType: string;
    NationalIdValue: string;
};

export type UpdateStudentRequest = {
    Id: string;
    Name: string;
    Email: string;
};
