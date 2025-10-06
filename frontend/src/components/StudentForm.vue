<template>
    <v-form ref="formRef" @submit.prevent="handleFormSubmit">
        <v-card max-width="344" :title="cardTitle">
            <v-container>
                <v-text-field v-model="student.name" color="primary" label="Nome" variant="underlined"></v-text-field>

                <v-text-field :rules="[...rules.email]" v-model="student.email" color="primary" label="E-mail"
                    variant="underlined"></v-text-field>

                <v-text-field :rules="[...rules.studentId]" :readonly="isEditing" v-model="student.studentId"
                    color="primary" label="RA" variant="underlined"></v-text-field>

                <v-text-field maxlength="11" :rules="[...rules.nationalId]" :readonly="isEditing"
                    v-model="student.nationalId" color="primary" label="CPF" placeholder="Digite o seu CPF."
                    variant="underlined"></v-text-field>
            </v-container>

            <v-divider></v-divider>

            <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="warning" variant="text" @click="closeRegisterStudent">
                    Cancelar
                    <v-icon icon="mdi-chevron-right" end></v-icon>
                </v-btn>

                <v-btn type="submit" color="success">
                    Salvar
                    <v-icon icon="mdi-chevron-right" end></v-icon>
                </v-btn>
            </v-card-actions>
        </v-card>
    </v-form>
</template>

<script setup lang="ts">
import { computed, ref, watch } from 'vue';
import { useStudentStore } from '@/stores/studentStore';
import { useForm } from '@/composables/useForm';
import { useNotificationStore } from '@/stores/notificationStore';

const studentStore = useStudentStore();
const notificationStore = useNotificationStore();
const emit = defineEmits(['registerStudent', 'closeRegisterStudent']);

const props = defineProps({
    mode: {
        type: String,
        default: 'create',
        validator: (value) => ['create', 'edit'].includes(value as string),
    }
});
const isEditing = computed(() => props.mode === 'edit');
const cardTitle = computed(() => isEditing.value ? 'Editar Aluno' : 'Cadastrar Novo Aluno');

const student = ref({
    id: '',
    name: '',
    email: '',
    studentId: '',
    nationalId: ''
});

if (isEditing) {
    const studentToEdit = studentStore.getStudentSelected;
    if (studentToEdit) {
        student.value = {
            id: studentToEdit.Id,
            name: studentToEdit.Name,
            email: studentToEdit.Email,
            studentId: studentToEdit.StudentId,
            nationalId: studentToEdit.NationalIdValue
        };
    }
}

const { values, formRef, rules, validate } = useForm(
    {
        student
    },
    {
        email: [
            (v: string) => !!v || 'Obrigatório',
            (v: string) => /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(v) || 'E-mail inválido'
        ],
        name: [
            (v: string) => !!v || 'O campo Nome é obrigatório'
        ],

        studentId: [
            (v: string) => !!v || 'O campo RA é obrigatório'
        ],

        nationalId: [
            (v: string) => !!v || 'O campo CPF é obrigatório',
            (v: string) => (v && v.length === 11) || 'O CPF deve ter 11 dígitos'
        ],
    }
);

const RegisterStudent = async () => {
    emit('registerStudent');
    try {
        await studentStore.addStudent({
            Name: student.value.name,
            Email: student.value.email,
            StudentId: student.value.studentId,
            NationalIdValue: student.value.nationalId,
            NationalIdType: "CPF"
        })

        notificationStore.showSnackbar('Aluno cadastrado com sucesso!', 'success');
        emit('closeRegisterStudent');
    } catch (error) {
        const errorMessage = 'Ocorreu um erro desconhecido no cadastro.';
        notificationStore.showSnackbar(errorMessage, 'error');
    }
};

const UpdateStudent = async () => {
    studentStore.updateStudent({
        Id: student.value.id,
        Name: student.value.name,
        Email: student.value.email
    });
    emit('closeRegisterStudent');
    notificationStore.showSnackbar('Aluno atualizado com sucesso!', 'success');
}

async function handleFormSubmit() {
    const { valid } = await validate();
    if (!valid) return;

    if (isEditing.value) {
        UpdateStudent();
    } else {
        RegisterStudent();
    }
}
function closeRegisterStudent() {
    studentStore.cleanSelection();
    emit('closeRegisterStudent');
}
</script>