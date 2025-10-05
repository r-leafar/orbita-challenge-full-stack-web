<template>
    <AppMenuBar :show-menu="true">
        <slot>
            <v-main>
                <v-container>
                    <h1>Gerenciar Alunos</h1>
                    <v-row>
                        <v-col cols="12" md="8">

                            <v-text-field :append-icon="'mdi-account-plus-outline'"
                                :append-inner-icon="'mdi-account-search-outline'" @click:append-inner="sendMessage"
                                @click:append="registerStudentDialog = true" v-model="searchTerm" label="Nome do aluno">
                            </v-text-field>
                            <v-dialog v-model="registerStudentDialog" :width="$vuetify.display.xs ? '320px' : '500px'">
                                <v-form ref="formRef" @submit.prevent="RegisterStudent">
                                    <v-card max-width="344" title="Registrar Aluno">
                                        <v-container>
                                            <v-text-field v-model="student.name" color="primary" label="Nome"
                                                variant="underlined"></v-text-field>

                                            <v-text-field :rules="[...rules.email]" v-model="student.email"
                                                color="primary" label="E-mail" variant="underlined"></v-text-field>

                                            <v-text-field :rules="[...rules.studentId]" v-model="student.studentId"
                                                color="primary" label="RA" variant="underlined"></v-text-field>

                                            <v-text-field maxlength="11" :rules="[...rules.nationalId]"
                                                v-model="student.nationalId" color="primary" label="CPF"
                                                placeholder="Digite o seu CPF." variant="underlined"></v-text-field>
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
                            </v-dialog>
                        </v-col>
                    </v-row>
                    <v-row>
                        <v-col v-for="item in studentStore.students" cols="12" sm="6" md="4" lg="3">
                            <v-card flat class="border" :key="item.id">
                                <v-card-subtitle class="pt-3">Informações</v-card-subtitle>
                                <v-card-text>
                                    <v-list density="compact">
                                        <v-list-item>
                                            <v-list-item-title class="font-weight-medium">Nome:</v-list-item-title>
                                            <v-list-item-subtitle>{{ item.name }}</v-list-item-subtitle>
                                        </v-list-item>

                                        <v-list-item>
                                            <v-list-item-title class="font-weight-medium">RA:</v-list-item-title>
                                            <v-list-item-subtitle>{{ item.studentId }}</v-list-item-subtitle>
                                        </v-list-item>

                                        <v-list-item>
                                            <v-list-item-title class="font-weight-medium">CPF:</v-list-item-title>
                                            <v-list-item-subtitle>{{ item.nationalIdValue }}</v-list-item-subtitle>
                                        </v-list-item>
                                    </v-list>

                                    <ConfirmDialog v-model="confirmDialog" title="Confirmar Ação"
                                        message="Tem certeza que deseja excluir este aluno?"
                                        @confirm="deleteStudent()" />
                                </v-card-text>

                                <v-card-actions>
                                    <v-btn variant="tonal" color="gray">Editar</v-btn>

                                    <v-btn @click="abrirConfirmDialog(item.id)" variant="tonal" color="red">Excluir
                                    </v-btn>
                                </v-card-actions>
                            </v-card>
                        </v-col>
                    </v-row>
                </v-container>
            </v-main>
        </slot>
    </AppMenuBar>
</template>

<script setup lang="ts">
import { onMounted, ref, reactive } from 'vue';
import { useStudentStore } from '@/stores/studentStore'
import { useForm } from '@/composables/useForm'

const snackbar = ref(false)
const text = ref('O estudante foi cadastrado com sucesso!')

const searchTerm = ref('');
const registerStudentDialog = ref(false);
const confirmDialog = ref(false);
const idForDelete = ref('');

const studentStore = useStudentStore();

const student = ref({
    name: '',
    email: '',
    studentId: '',
    nationalId: ''
})
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
)
function abrirConfirmDialog(id: string) {
    confirmDialog.value = true;
    idForDelete.value = id;
    console.log(idForDelete.value);
}
function deleteStudent() {
    studentStore.deleteStudent(idForDelete.value);
}

function sendMessage() {
    alert(`Você pesquisou por: ${searchTerm.value}`);
}
function clearRegistrationForm() {
    (Object.keys(student.value) as Array<keyof typeof student.value>).forEach(key => {
        student.value[key] = '';
    });
}

function closeRegisterStudent() {
    registerStudentDialog.value = false;
    clearRegistrationForm();
}

const RegisterStudent = async () => {
    const { valid } = await validate()
    if (!valid) return

    await studentStore.addStudent({
        Name: student.value.name,
        Email: student.value.email,
        StudentId: student.value.studentId,
        NationalIdValue: student.value.nationalId,
        NationalIdType: "CPF"
    })

    closeRegisterStudent()
    snackbar.value = true
}

onMounted(() => {
    studentStore.getStudentsPaged();
});
</script>