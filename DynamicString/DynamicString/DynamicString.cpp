#include "pch.h"
#include <iostream>
#include <vector>

using namespace std;

//класс динамической строки
class DynamicString
{
public:
	size_t size;
	char *DS;
	//конструктор по умолчанию
	DynamicString()
	{
		size = 0;
		this->DS = new char[size];
	}

	//конструктор
	DynamicString(const char *const DS)
	{
		size = strlen(DS) + 1;
		this->DS = new char[size];
		strcpy_s(this->DS, size, DS);
	}
	
	//перегрузка оператора присваивания
	DynamicString &operator = (const DynamicString &other)
	{
		//проверка на присваивание самому себе
		if (this == &other)
		{
			return *this;
		}
		//присваивание
		this->size = other.size;
		if (this->DS != nullptr)
		{
			delete[] this->DS;
		}
		this->DS = new char[size];
		for (size_t i = 0; i < other.size; i++)
		{
			this->DS[i] = other.DS[i];
		}
		return *this;
	}

	//конструктор копирования
	DynamicString(const DynamicString &other)
	{
		this->size = other.size;
		this->DS = new char[size];
		for (size_t i = 0; i < other.size; i++)
		{
			this->DS[i] = other.DS[i];
		}
	}

	//конструктор перемещения (move-семантика)
	DynamicString(DynamicString &&other)
	{
		DS = other.DS;
		size = other.size;
		other.DS = nullptr;
		other.size = 0;
	}

	//тест move-семантики (функция, принимающая объект класса по значению)
	void TestMove(DynamicString other)
	{
		cout << endl;
		for (size_t i = 0; i < other.size; i++)
		{
			cout << other.DS[i];
		}
		cout << endl;
	}

	//вывод строки на экран
	void Print()
	{
		for (size_t i = 0; i < this->size; i++)
		{
			cout << DS[i];
		}
		cout << endl;
	}

	//деструктор
	~DynamicString()
	{
		delete[]DS;
	}
};

//"внешний" оператор сложения двух строк
DynamicString operator + (DynamicString DS1, DynamicString DS2)
{
	auto DSValuesize = DS1.size + DS2.size - 1;
	auto DSValue = new char[DSValuesize];
	DynamicString DS = DynamicString(DSValue);
	delete[]DSValue;
	DS.size = DSValuesize;
	for (size_t i = 0; i < DS1.size; i++)
	{
		DS.DS[i] = DS1.DS[i];
	}
	for (size_t j = DS1.size - 1; j < DS.size; j++)
	{
		DS.DS[j] = DS2.DS[j - DS1.size + 1];
	}
	return DS;
}


int main()
{
	setlocale(LC_ALL, "Rus");
	
	vector<DynamicString> RuAlphabet = {"Ru", "Aа", "Бб", "Вв", "Гг", "Дд", "Ее", "Ёё", "Жж", "Зз", "Ии", "Йй", "Кк", "Лл", "Мм", "Нн", "Оо", "Пп", "Рр", "Сс", "Тт", "Уу", "Фф", "Хх", "Цц", "Чч", "Шш", "Щщ", "Ъъ", "Ыы", "Ьь", "Ээ", "Юю", "Яя"};
	vector<DynamicString> Test = { "Дед", "Баба", "лягушка", "жаба" };

	int Check = 0;
	DynamicString temp;
	size_t ds1 = 0;
	size_t ds2 = 0;

	//вывод неотсортированного списка
	for (size_t i = 0; i < Test.size(); i++)
	{
		Test[i].Print();
	}

	//проверка на допустимые символы в списке 
	for (size_t i = 0; i < Test.size(); i++)
	{
		Check = 0;
		for (size_t j = 0; j < Test[i].size; j++)
		{
			for (size_t a = 1; a < RuAlphabet.size(); a++)
			{
				if ((Test[i].DS[j] == RuAlphabet[a].DS[0]) || (Test[i].DS[j] == RuAlphabet[a].DS[1]))
				{
					Check++;
				}
			}
		}
		if (Check + 1 != Test[i].size)
		{
			cout << "Данные неверны." << endl;
			return 1;
		}
	}

	//сортировка
	for (size_t i = 0; i < Test.size() - 1; i++)
	{
		ds1 = 0;
		ds2 = 0;
		for (size_t j = 0; j < Test.size() - i - 1; j++)
		{
			ds1 = 0;
			ds2 = 0;
			if (Test[j].size < Test[j + 1].size)
			{
				for (size_t symbol = 0; symbol < Test[j].size; symbol++)
				{
					ds1 = 0;
					ds2 = 0;
					for (size_t a = 1; a < RuAlphabet.size(); a++)
					{
						if ((Test[j].DS[symbol] == RuAlphabet[a].DS[0]) || (Test[j].DS[symbol] == RuAlphabet[a].DS[1]))
						{
							ds1 = a;
						}
						if ((Test[j + 1].DS[symbol] == RuAlphabet[a].DS[0]) || (Test[j + 1].DS[symbol] == RuAlphabet[a].DS[1]))
						{
							ds2 = a;
						}
						if ((ds1 != 0) && (ds2 != 0))
						{
							if (ds1 < ds2)
							{
								temp = Test[j];
								Test[j] = Test[j + 1];
								Test[j + 1] = temp;
								a = RuAlphabet.size();
							    symbol = Test[j].size;
							}
							if (ds1 > ds2)
							{
								a = RuAlphabet.size();
								symbol = Test[j].size;
							}
							if (ds1 == ds2)
							{
								if (symbol == Test[j].size - 1)
								{
									temp = Test[j];
									Test[j] = Test[j + 1];
									Test[j + 1] = temp;
									a = RuAlphabet.size();
									symbol = Test[j].size;
								}
								else
								{
									a = RuAlphabet.size();
								}
							}
						}
					}
				}
			}
			if (Test[j].size > Test[j + 1].size)
			{
				for (size_t symbol = 0; symbol < Test[j + 1].size; symbol++)
				{
					ds1 = 0;
					ds2 = 0;
					for (size_t a = 1; a < RuAlphabet.size(); a++)
					{
						if ((Test[j].DS[symbol] == RuAlphabet[a].DS[0]) || (Test[j].DS[symbol] == RuAlphabet[a].DS[1]))
						{
							ds1 = a;
						}
						if ((Test[j + 1].DS[symbol] == RuAlphabet[a].DS[0]) || (Test[j + 1].DS[symbol] == RuAlphabet[a].DS[1]))
						{
							ds2 = a;
						}
						if ((ds1 != 0) && (ds2 != 0))
						{
							if (ds1 < ds2)
							{
								temp = Test[j];
								Test[j] = Test[j + 1];
								Test[j + 1] = temp;
								a = RuAlphabet.size();
								symbol = Test[j].size;
							}
							if (ds1 > ds2)
							{
								a = RuAlphabet.size();
								symbol = Test[j].size;
							}
							if (ds1 == ds2)
							{
								if (symbol == Test[j + 1].size - 1)
								{
									a = RuAlphabet.size();
									symbol = Test[j + 1].size;
								}
								else
								{
									a = RuAlphabet.size();
								}
							}
						}
					}
				}
			}
			if (Test[j].size == Test[j + 1].size)
			{

				for (size_t symbol = 0; symbol < Test[j].size; symbol++)
				{
					ds1 = 0;
					ds2 = 0;
					for (size_t a = 1; a < RuAlphabet.size(); a++)
					{
						if ((Test[j].DS[symbol] == RuAlphabet[a].DS[0]) || (Test[j].DS[symbol] == RuAlphabet[a].DS[1]))
						{
							ds1 = a;
						}
						if ((Test[j + 1].DS[symbol] == RuAlphabet[a].DS[0]) || (Test[j + 1].DS[symbol] == RuAlphabet[a].DS[1]))
						{
							ds2 = a;
						}
						if ((ds1 != 0) && (ds2 != 0))
						{
							if (ds1 < ds2)
							{
								temp = Test[j];
								Test[j] = Test[j + 1];
								Test[j + 1] = temp;
								a = RuAlphabet.size();
								symbol = Test[j].size;
							}
							if (ds1 > ds2)
							{
								a = RuAlphabet.size();
								symbol = Test[j].size;
							}
							if (ds1 == ds2)
							{
								if (symbol == Test[j].size - 1)
								{
									a = RuAlphabet.size();
									symbol = Test[j].size;
								}
								else
								{
									a = RuAlphabet.size();
								}
							}
						}
					}
				}
			}
		}
	}
	
	//вывод отсортированного списка
	cout << endl;
	for (size_t i = 0; i < Test.size(); i++)
	{
		Test[i].Print();
	}

	return 0;
}