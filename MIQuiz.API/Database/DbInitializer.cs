using MIQuizAPI.Database.Context;
using MIQuizAPI.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIQuizAPI.Database
{
    public class DbInitializer
    {
        public static void Initialize( MIQuizContext context ) {
            context.Database.EnsureCreated();

            var demoUser = "LaneBoy";

            // Check if records exist... if so then no need to run seed methods.
            if(context.Users.Any() || context.Quizes.Any() || context.Questions.Any() || context.Answers.Any() ) {
                return;
            }

            // Seeds of proserity
            var users = new User[] {
                new User{ IsActive=true, FirstName="Demo", LastName="User", UserName=demoUser, Role=0 }
            };
            foreach( User u in users ) {
                context.Users.Add( u );
            }
            context.SaveChanges();

            var demoUserId = users.Single( u => u.UserName.Equals( demoUser ) ).UserId;

            var quizDefs = new QuizDef[] {
                new QuizDef{ Name="Demo Quiz YO!", Description="This is a test... it is only a test.", Instructions="Do the thing.", GradingCriteria="Largely based on feels, you know like college.", IsActive=true, Order=0, UserId=demoUserId }
            };
            foreach( QuizDef qd in quizDefs ) {
                context.Quizes.Add( qd );
            }
            context.SaveChanges();

            var questionDefs = new QuestionDef[] {
                new QuestionDef{ IsActive=true, Type="MC", Text="Multiple Choice Question With Single Correct Answer?" },
                new QuestionDef{ IsActive=true, Type="MC", Text="Multiple Choice Question With Several Correct Answers?" },
                new QuestionDef{ IsActive=true, Type="MC", Text="Textual Question With Single Correct Answer... How many correct answers are there to this question?" },
                new QuestionDef{ IsActive=true, Type="MC", Text="Textual Question With Several Correct Answers... Can this question have more than one correct answer?" },
                new QuestionDef{ IsActive=true, Type="MC", Text="True/False Type Question With Traditional Answers?" },
                new QuestionDef{ IsActive=true, Type="MC", Text="True/False Type Question With Different Text For Answers?" }
            };
            foreach( QuestionDef qd in questionDefs ) {
                context.Questions.Add( qd );
            }
            context.SaveChanges();

            var answerDefs = new AnswerDef[] {
                new AnswerDef{ IsActive=true, Text="This is a multiple choice type question with one correct answer." },
                new AnswerDef{ IsActive=true, Text="This is not a multiple choice type question." },
                new AnswerDef{ IsActive=true, Text="Pigs can fly every bit as well as an web application developer." },
                new AnswerDef{ IsActive=true, Text="This is not the API you are looking for..." },
                new AnswerDef{ IsActive=true, Text="This is a multiple choice type question with several correct answers." },
                new AnswerDef{ IsActive=true, Text="This is not a multiple choice type question." },
                new AnswerDef{ IsActive=true, Text="This answer could be correct as well." },
                new AnswerDef{ IsActive=true, Text="This is not the API you are looking for..." },
                new AnswerDef{ IsActive=true, Text="one" },
                new AnswerDef{ IsActive=true, Text="1" },
                new AnswerDef{ IsActive=true, Text="yes" },
                new AnswerDef{ IsActive=true, Text="yup" },
                new AnswerDef{ IsActive=true, Text="absolutely" },
                new AnswerDef{ IsActive=true, Text="for sursies" },
                new AnswerDef{ IsActive=true, Text="True" },
                new AnswerDef{ IsActive=true, Text="False" },
                new AnswerDef{ IsActive=true, Text="Whaaaaaaattttt..." },
                new AnswerDef{ IsActive=true, Text="Oh for sure it is!" },
            };
            foreach( AnswerDef ad in answerDefs ) {
                context.Answers.Add( ad );
            }
            context.SaveChanges();

            var quizToQuestion = new JoinQuizQuestion[] {
                new JoinQuizQuestion{ Quiz=quizDefs[0], Question=questionDefs[0], QuestionOrder=0 }, //Multiple Choice Question With Single Correct Answer?
                new JoinQuizQuestion{ Quiz=quizDefs[0], Question=questionDefs[1], QuestionOrder=1 }, //Multiple Choice Question With Several Correct Answers?
                new JoinQuizQuestion{ Quiz=quizDefs[0], Question=questionDefs[2], QuestionOrder=2 }, //Textual Question With Single Correct Answer... How many correct answers are there to this question?
                new JoinQuizQuestion{ Quiz=quizDefs[0], Question=questionDefs[3], QuestionOrder=3 }, //Textual Question With Several Correct Answers... Can this question have more than one correct answer?
                new JoinQuizQuestion{ Quiz=quizDefs[0], Question=questionDefs[4], QuestionOrder=4 }, //True/False Type Question With Traditional Answers?
                new JoinQuizQuestion{ Quiz=quizDefs[0], Question=questionDefs[5], QuestionOrder=5 }  //True/False Type Question With Different Text For Answers?
            };

            // Add stuff to set up JoinQuizQuestion (QuizQuestionTbl)
            context.AddRange( quizToQuestion );
            context.SaveChanges();

            var questionToAnswer = new JoinQuestionAnswer[] {
                //Multiple Choice Question With Single Correct Answer?
                new JoinQuestionAnswer{ Question=questionDefs[0], Answer = answerDefs[0], AnswerOrder=0, IsCorrectAnswer=true },  //This is a multiple choice type question with one correct answer.
                new JoinQuestionAnswer{ Question=questionDefs[0], Answer = answerDefs[1], AnswerOrder=1, IsCorrectAnswer=false }, //This is not a multiple choice type question.
                new JoinQuestionAnswer{ Question=questionDefs[0], Answer = answerDefs[2], AnswerOrder=2, IsCorrectAnswer=false }, //Pigs can fly every bit as well as an web application developer.
                new JoinQuestionAnswer{ Question=questionDefs[0], Answer = answerDefs[3], AnswerOrder=3, IsCorrectAnswer=false }, //This is not the API you are looking for...
                //Multiple Choice Question With Several Correct Answers?
                new JoinQuestionAnswer{ Question=questionDefs[1], Answer = answerDefs[4], AnswerOrder=0, IsCorrectAnswer=true },  //This is a multiple choice type question with several correct answers.
                new JoinQuestionAnswer{ Question=questionDefs[1], Answer = answerDefs[5], AnswerOrder=1, IsCorrectAnswer=false }, //This is not a multiple choice type question.
                new JoinQuestionAnswer{ Question=questionDefs[1], Answer = answerDefs[6], AnswerOrder=2, IsCorrectAnswer=true },  //This answer could be correct as well.
                new JoinQuestionAnswer{ Question=questionDefs[1], Answer = answerDefs[7], AnswerOrder=3, IsCorrectAnswer=false }, //This is not the API you are looking for...
                //Textual Question With Single Correct Answer... How many correct answers are there to this question?
                new JoinQuestionAnswer{ Question=questionDefs[2], Answer = answerDefs[8], AnswerOrder=0, IsCorrectAnswer=true }, //one
                new JoinQuestionAnswer{ Question=questionDefs[2], Answer = answerDefs[9], AnswerOrder=1, IsCorrectAnswer=true }, //1
                //Textual Question With Several Correct Answers... Can this question have more than one correct answer?
                new JoinQuestionAnswer{ Question=questionDefs[3], Answer = answerDefs[10], AnswerOrder=0, IsCorrectAnswer=true }, //yes
                new JoinQuestionAnswer{ Question=questionDefs[3], Answer = answerDefs[11], AnswerOrder=1, IsCorrectAnswer=true }, //yup
                new JoinQuestionAnswer{ Question=questionDefs[3], Answer = answerDefs[12], AnswerOrder=2, IsCorrectAnswer=true }, //absolutely
                new JoinQuestionAnswer{ Question=questionDefs[3], Answer = answerDefs[13], AnswerOrder=2, IsCorrectAnswer=true }, //for sursies
                //True/False Type Question With Traditional Answers?
                new JoinQuestionAnswer{ Question=questionDefs[4], Answer = answerDefs[14], AnswerOrder=0, IsCorrectAnswer=true }, //True
                new JoinQuestionAnswer{ Question=questionDefs[4], Answer = answerDefs[15], AnswerOrder=1, IsCorrectAnswer=true }, //False
                //True/False Type Question With Different Text For Answers?
                new JoinQuestionAnswer{ Question=questionDefs[5], Answer = answerDefs[16], AnswerOrder=0, IsCorrectAnswer=true }, //Whaaaaaaattttt...
                new JoinQuestionAnswer{ Question=questionDefs[5], Answer = answerDefs[17], AnswerOrder=1, IsCorrectAnswer=true }  //Oh for sure it is!
            };





            // Add stuff to set up JoinQuestionAnswer (QuestionAnswerTbl)
            context.AddRange( questionToAnswer );
            context.SaveChanges();

        }

    }
}
