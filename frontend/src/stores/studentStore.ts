/// <reference types="node" />
import { defineStore } from 'pinia'
import type { Student, CreateStudentRequest, UpdateStudentRequest } from '@/types/student';
import type { PagedResponse } from '@/types/paged-response';
import axios from "axios";

const PAGE_SIZE = 8;
const API_BASE_URL = import.meta.env.VITE_APP_API_URL;

export const useStudentStore = defineStore('student', {
    state: () => ({
        students: [] as Student[],
        studentSelected: null as Student | null,
    }),
    actions: {
        async addStudent(student: CreateStudentRequest) {
            const response = await axios.post<Student>(
                `${API_BASE_URL}/api/v1/students`,
                student
            );

            if (response.status === 201 || response.status === 200) {
                // Atualiza o array de forma reativa
                this.students = [...this.students, response.data];
                this.students = [...this.students.slice(0, PAGE_SIZE)];
            }
        },
        async updateStudent(student: UpdateStudentRequest) {
            const response = await axios.put<Student>(
                `${API_BASE_URL}/api/v1/students`,
                student
            );
            if (response.status === 204 || response.status === 200) {
                this.students = this.students.map(s => {
                    // 1. Encontra o aluno a ser atualizado
                    if (s.Id === student.Id) {
                        return {
                            ...s,        // Pega todas as propriedades do aluno ORIGINAL
                            ...student,  // Sobrescreve apenas as propriedades existentes no payload
                        };
                    }
                    return s;
                });
            }
        },
        async setStudentToEdit(student: Student) {
            this.studentSelected = student;
        },
        async cleanSelection() {
            this.studentSelected = null;
        },

        async getStudentsPaged(page: number = 1) {
            const response = await axios.get<PagedResponse<Student>>(
                `${API_BASE_URL}/api/v1/students/${page}/${PAGE_SIZE}`
            );

            if (response.data.Data && response.data.Data.length > 0) {
                this.students = [...response.data.Data]; // nova referência
            } else {
                this.students = [];
            }
        },
        async getStudentsByNamePaged(name: string, page: number = 1) {
            const response = await axios.get<PagedResponse<Student>>(
                `${API_BASE_URL}/api/v1/students/${name}/${page}/${PAGE_SIZE}`
            );

            if (response.data.Data && response.data.Data.length > 0) {
                this.students = [...response.data.Data]; // nova referência
            } else {
                this.students = [];
            }
        }
        ,
        async deleteStudent(id: string) {
            const response = await axios.delete(`${API_BASE_URL}/api/v1/students/${id}`);
            if (response.status === 200 || response.status === 204) {
                this.students = this.students.filter(student => student.Id !== id);
            }
        }
    },
    getters: {
        getStudentSelected: (state) => state.studentSelected
    },
    persist: true,
});

