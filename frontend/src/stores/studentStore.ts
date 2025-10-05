/// <reference types="node" />
import { defineStore } from 'pinia'
import type { Student, CreateStudentRequest } from '@/types/student';
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
            }
        },

        async getStudentsPaged(page: number = 1) {
            const response = await axios.get<PagedResponse<Student>>(
                `${API_BASE_URL}/api/v1/students/${page}/${PAGE_SIZE}`
            );

            if (response.data.data && response.data.data.length > 0) {
                this.students = [...response.data.data]; // nova referência
            } else {
                this.students = [];
            }
        },
        async deleteStudent(id: string) {
            const response = await axios.delete(`${API_BASE_URL}/api/v1/students/${id}`);
            if (response.status === 200 || response.status === 204) {
                this.students = this.students.filter(student => student.id !== id);
            }
        }
    },
    getters: {
        getStudentSelected: (state) => state.studentSelected
    },
    persist: true,
});

