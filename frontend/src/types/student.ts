export type Student = {
    id: string;
    name: string;
    email: string;
    studentId: string;
    nationalIdValue: string;
};

export type CreateStudentRequest = {
    Name: string;
    Email: string;
    StudentId: string;
    NationalIdType: string;
    NationalIdValue: string;
};